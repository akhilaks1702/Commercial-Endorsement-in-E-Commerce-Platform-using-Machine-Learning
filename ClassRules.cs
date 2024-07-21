using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace DATAMINING_ASSOCIATIONRULE
{
    public class ClassRules : IComparable<ClassRules>
    {
        string Combination, Remaining;
        double _confidence;

        public ClassRules(string Combination, string Remaining, double Confidence)
        {
            this.Combination = Combination;
            this.Remaining = Remaining;
            this._confidence = Confidence;
        }

        public string X
        {
            get
            {
                return Combination;
            }
        }
        public string Y
        {
            get
            {
                return Remaining;
            }
        }

        public double Confidence
        {
            get
            {
                return _confidence;
            }
        }



        #region IComparable<classRules> Members

        public int CompareTo(ClassRules other)
        {
            return X.CompareTo(other.X);
        }

        #endregion
    }
}
