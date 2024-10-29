using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App4.Models
{
    public class Sales
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Fecha { get; set; }
        public string DescripcionVenta => $"Se vendieron {Cantidad} unidades a ${Precio} c/u.";
    }

}