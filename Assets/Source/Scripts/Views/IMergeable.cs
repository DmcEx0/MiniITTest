using System;

namespace MiniIT.Views
{
    public interface IMergeable
    {
        public Func<IMergeable, IMergeable, bool> Merging { get; set; }
    }
}
