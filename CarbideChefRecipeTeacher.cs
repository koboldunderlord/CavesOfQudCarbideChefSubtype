using System;
using System.Text;
using System.Collections.Generic;
using XRL.Rules;
using XRL.Core;
using XRL.World.Parts.Effects;
using XRL.UI;

namespace XRL.World.Parts
{
    [Serializable]
    public class CarbideChefRecipeTeacher : IActivePart
    {
        // Blueprint parameter
        public int Recipes = 3;

        public CarbideChefRecipeTeacher()
        {

        }

        public override bool AllowStaticRegistration()
        {
            return false;
        }

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "EndTurn");
            base.Register(Object);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "EndTurn")
            {
                if (ParentObject.HasPart("OpeningStory")) {
                    return true;
                }
                if (!ParentObject.HasEffect("Inspired")) 
                {
                    if (ParentObject.IsPlayer ()) {
                        Popup.Show("Your training as a carbide chef has inspired you to invent a meal!");
                    }
                    ParentObject.ApplyEffect(new Inspired(Calendar.turnsPerDay * 2));
                    Recipes--;
                }

                if (Recipes == 0)
                {
                    ParentObject.RemovePart(this);
                }
            }
            return true;
        }
    }
}
