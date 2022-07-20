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

        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
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
        /// GET
        /// </summary>
        /// <returns></returns>
        public ProductModel GetProductsById(int id)
        {
            //ProductModel responce = new ProductModel();
            var row  = _dataContext.Products.Where(p => p.id.Equals(id)).FirstOrDefault();
            return new ProductModel() { 
                brand = row?.brand,
                id = (int)row?.id,
                name = row?.name,
                price = (int)row?.price,
                size = (int)row?.size
            };
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

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrder(int id)
        {
            var order = _dataContext.Orders.Where(o => o.id.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                _dataContext.Orders.Remove(order);
                _dataContext.SaveChanges();
            }
        }
    }
}
