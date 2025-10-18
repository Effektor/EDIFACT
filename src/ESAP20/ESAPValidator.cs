using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    class ESAPValidator
    {
        public bool Validate(IEdifactSegment element)
        {
            switch (element)
            {
                case BGM b: return ValidateBGM(b);
                default:return true;
            }
        }

        private bool ValidateBGM(BGM element)
        {
            throw new NotImplementedException();
        }
    }
}
