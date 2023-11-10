//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Integrador.Map.Locations;

namespace TP_Integrador
{
    class Quarter : Location
    {
        List<Operator> operators;

        public Quarter(string name, int coordX, int coordY) : base(name, coordX, coordY)
        {            
            operators = new List<Operator>();
            type = LocationTypes.Quarter;
        }        

        public List<Operator> GetOperators()
        {
            return operators;
        }

        public List<Operator> GetOperators(LocationTypes location)
        {
            return operators.FindAll(o => o.GetLocation().Equals(location));
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

            Operator notReturned = operators.Find(o => !o.GetLocation().Equals(LocationTypes.Cuartel));

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

        public bool MakeOperatorTravel(int id, LocationTypes location)
        {
            bool done = false;
            int index = operators.FindIndex(o => o.GetId() == id);

            if (index > -1)
            {
                done = operators[index].Travel(location);
            }           

            return done;
        }

        public bool ChangeOperatorState(int id, bool standBy)
        {
            bool done = false;
            int index = operators.FindIndex(o => o.GetId() == id);

            if (index > -1)
            {
                operators[index].ChangeState(standBy);
                done = true;
            }

            return done;
        }

        public int GetMaxId()
        {
            int id = 0;
            foreach (Operator op in operators) 
            { 
                if (op.GetId() > id)
                {
                    id = op.GetId();
                }
            }
            return id;
        }
    }
}
