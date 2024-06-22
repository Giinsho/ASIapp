using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ASIapp.Classes
{


    public class Disease : CellObject
    {

        public int emergencyHops = 0;

        public Disease() { }

        public Disease(CellObject cell) : this()
        {
            ID = cell.ID;
            GLOBAL_ID = cell.GLOBAL_ID;
        }
    }
}