using EntityPG.EFCore;

namespace EntityPG.Model
{
    public class DbHelper
    {
        private DataContext _dataContext;
        public DbHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public List<ProductModel> GetProducts()
        { 
            List<ProductModel> responce = new List<ProductModel>();
            var dataList = _dataContext.Products.ToList();
            dataList.ForEach(row => responce.Add(new ProductModel() {
                brand = row.brand,
                id = row.id,
                name = row.name,
                price = row.price,
                size = row.size
            }));
            return responce;
        }

        /// <summary>
        /// it serve the POST/PUT/PATH
        /// </summary>
        public void SaveOrder(OrderModel orderModel)
        { 
            Order dbTable = new Order();
            if (orderModel.id > 0)
            { 
                //put
                dbTable = _dataContext.Orders.Where(o => o.id.Equals(orderModel.id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.phone = orderModel.phone;
                    dbTable.address = orderModel.address;
                }
                else 
                {
                    //Post
                    dbTable.phone = orderModel.phone;
                    dbTable.name = orderModel.name;
                    dbTable.address = orderModel.address;
                    dbTable.product = _dataContext.Products.Where(o => o.id.Equals(orderModel.product_id)).FirstOrDefault();
                    _dataContext.Orders.Add(dbTable);
                }
                _dataContext.SaveChanges();
            }
        }
    }
}
