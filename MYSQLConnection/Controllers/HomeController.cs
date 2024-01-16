using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MYSQLConnection.Models;
using System.Diagnostics;

namespace MYSQLConnection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Person> persons = new List<Person>();
            //Database Connection

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=DBConnection;port=3306;password=manoj22561"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from person",con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    // extract your data
                    Person person = new Person();
                    person.Id = Convert.ToInt32(reader["id"]);
                    person.FirstName = reader["first_name"].ToString();
                    person.LastName = reader["last_name"].ToString();
                    person.Age = Convert.ToInt32(reader["age"]);

                    persons.Add(person);
                }
                reader.Close();
            }


            return View(persons);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
