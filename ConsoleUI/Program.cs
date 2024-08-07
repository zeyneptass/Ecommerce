// See https://aka.ms/new-console-template for more information


using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

#region GetAllDatas
//GetAllProducts();
static void GetAllProducts()
{
    ProductManager productManager = new ProductManager(new EfProductDal());


    foreach (var product in productManager.GetAll())
    {
        Console.WriteLine(product.ProductName);
    }
}

//GetAllShippingInfos();

static void GetAllShippingInfos()
{
    ShippingInfoManager shippingInfoManager = new ShippingInfoManager(new EfShippingInfoDal());
    foreach (var shippingInfo in shippingInfoManager.GetAll())
    {
        Console.WriteLine(shippingInfo.Carrier);
    }
}

//GetAllOrders();

static void GetAllOrders()
{
    OrderManager orderManager = new OrderManager(new EfOrderDal());
    foreach (var order in orderManager.GetAll())
    {
        Console.WriteLine("Sipariş adresi : " + order.ShippingAddress);
    }
}

//GetAllCategories();

static void GetAllCategories()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (var category in categoryManager.GetAll())
    {
        Console.WriteLine(category.CategoryName);
    }
}
#endregion