using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Configuration;
using System.Configuration;

namespace PushNotification
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static IEnumerable<Products> GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
            {
                connection.Open();
                //using (SqlCommand command = new SqlCommand(@"SELECT  [ProductID],[Name],[UnitPrice],[Quantity] FROM [SignalRDemo].[dbo].[Products]", connection))
                using (SqlCommand command = new SqlCommand(@"select * from SignalRDemo", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;
                    SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new Products()
                            {
                                id = x.GetInt32(0),
                                Name = x.GetString(1),
                                //PricDecimal = x.GetDecimal(2),
                                PricDecimal = x.GetInt32(0),
                                //QuantDecimal = x.GetDecimal(3)
                                QuantDecimal = x.GetInt32(3)
                            }).ToList();



                }
            }
        }
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            MyHub.Show();
        }
    }
}