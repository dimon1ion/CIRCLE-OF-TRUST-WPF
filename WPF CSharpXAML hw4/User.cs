using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CSharpXAML_hw4
{
    public class User
    {
        public string Name { get; set; }
        public string[] Rates { get; set; }
        public User()
        {

        }
        public User(string name)
        {
            this.Name = name;
            Rates = new string[5];
            for (int i = 0; i < 5; i++)
            {
                Rates[i] = AppDomain.CurrentDomain.BaseDirectory + "\\StarDisable.png";
            }
        }
    }
}
