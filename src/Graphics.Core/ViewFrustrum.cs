using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Core
{
    public class ViewFrustrum : Entity
    {
        public ViewFrustrum(double near, double far, double fov)
        {
        }

        public double Near { get; set; }
        public double Far { get; set; }
    }
}