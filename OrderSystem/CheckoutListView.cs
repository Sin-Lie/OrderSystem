using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 点菜系统
{
    public partial class CheckoutListView : ListView
    {
        //构造函数
        public CheckoutListView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void show()
        {

        }

    }
}
