using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolvers.Ninject;
using Northwind.DataAccessLayer.Concrete.EntityFrameWork;
using Northwind.DataAccessLayer.Concrete.EntityFramWork;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLayerAppDemo15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _productServices = InstanceFactory.GetInstance<IProductServices>();
            _categoryServices = InstanceFactory.GetInstance<ICategoryServices>();
        }
        IProductServices _productServices;
        ICategoryServices _categoryServices;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryServices.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryServices.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxCategoryIdUpdate.DataSource = _categoryServices.GetAll();
            cbxCategoryIdUpdate.DisplayMember = "CategoryName";
            cbxCategoryIdUpdate.ValueMember = "CategoryId";
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productServices.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productServices.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedIndex));
            }
            catch 
            {

            }
        }
        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(tbxProductName.Text))
            {
                dgwProduct.DataSource = _productServices.GetProductsByProdutName(tbxProductName.Text);
            }
            else
            {
                LoadProducts();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productServices.Add(new Product
            {
                CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                ProductName = tbxProductName2.Text,
                UnitPrice=Convert.ToDecimal(tbxUnitPrice.Text),
                QuantityPerUnit=tbxQuantityPerUnit.Text,
                UnitsInStock=Convert.ToInt16(tbxStock.Text)
            });
            MessageBox.Show("Ürün Kaydedildi!");
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productServices.Update(new Product
            {
                ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                ProductName = tbxProductNameUpdate.Text,
                CategoryId = Convert.ToInt32(cbxCategoryIdUpdate.SelectedValue),
                UnitPrice=Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                QuantityPerUnit=tbxQuantityPerUnitUpdate.Text,
                UnitsInStock=Convert.ToInt16(tbxStockUpdate.Text),

            });
            MessageBox.Show("Ürün Güncellendi!");
            LoadProducts();
        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row=dgwProduct.CurrentRow;
            tbxProductNameUpdate.Text =row.Cells[1].Value.ToString();
            cbxCategoryIdUpdate.SelectedValue = row.Cells[2].Value;
            tbxUnitPriceUpdate.Text = row.Cells[3].Value.ToString();
            tbxQuantityPerUnitUpdate.Text = row.Cells[4].Value.ToString();
            tbxStockUpdate.Text = row.Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgwProduct.CurrentRow!=null)
            {
                try
                {
                    _productServices.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    });
                    MessageBox.Show("Ürün Silindi!");
                    LoadProducts();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
            }
            
        }
    }
}
