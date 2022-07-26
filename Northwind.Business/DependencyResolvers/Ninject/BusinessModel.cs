using Ninject.Modules;
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccessLayer.Abstract;
using Northwind.DataAccessLayer.Concrete.EntityFrameWork;
using Northwind.DataAccessLayer.Concrete.EntityFramWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.DependencyResolvers.Ninject
{
    public class BusinessModel : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();
            Bind<IProductServices>().To<ProductManager>().InSingletonScope();

            Bind<ICategoryDal>().To<EfCategoryDal>().InSingletonScope();
            Bind<ICategoryServices>().To<CategoryManager>().InSingletonScope();
        }
    }
}
