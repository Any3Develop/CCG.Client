using System.Collections.Generic;

namespace Services.UIService.Abstractions
{
    public interface IDisplayOptionFactory
    {
	    void Init(IUIService service);
        IDisplayOption CreateShow(params object[] args);
        IDisplayOption CreateHide(params object[] args);
        IDisplayOption CreateWrapper(IEnumerable<IDisplayOption> options);
    }
}