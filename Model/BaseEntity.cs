using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    abstract public class BaseEntity
    {
        private int id;   // the ID number of the element

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}
