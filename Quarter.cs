using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class Quarter
    {
        List<Operator> operators;
        string quarterName;

        public Quarter(string name)
        {
            quarterName = name;
            operators = new List<Operator>();
        }

        public List<Operator> GetOperators()
        {
            return operators;
        }

        public List<Operator> GetOperators(string localization)
        {
            return operators.FindAll(o => o.GetLocalization().Equals(localization.ToUpper()));
        }
        
        public Operator GetOperator(int id)
        {
            return operators.Find(o => o.GetId() == id);
        }

        public bool TotalRecall()
        {
            bool done = false;

            foreach (Operator op in operators)
            {
                op.ReturnToQuarter();
            }

            Operator notReturned = operators.Find(o => !o.GetLocalization().Equals("CUARTEL"));

            if (notReturned == null)
            {
                done = true;
            }

            return done;
        }

        public bool AddOperator(Operator op)
        {
            bool done = false;

            Operator sameId = operators.Find(o => o.GetId() == op.GetId());

            if (sameId == null)
            {
                operators.Add(op);
                done = true;
            }

            return done;
        }

        public bool RemoveOperator(int id)
        {
            Operator op = operators.Find(o => o.GetId() == id);
            return operators.Remove(op);
        }

        public bool MakeOperatorTravel(int id, string localization, int kmToTravel)
        {
            bool done = false;
            int index = operators.FindIndex(o => o.GetId() == id);

            if (index > -1)
            {
                done = operators[index].Travel(localization, kmToTravel);
            }           

            return done;
        }
    }
}
