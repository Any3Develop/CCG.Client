using System.Collections.Generic;
using System.Linq;
using Services.UIService.Abstractions;
using Services.UIService.Data;

namespace Services.UIService
{
    public class DisplayOptionsFactory : IDisplayOptionFactory
    {
	    private readonly IUIRoot uiRoot;
	    private readonly IUIFullFadePresenter fullFade;
	    private IUIService uiService;

	    public DisplayOptionsFactory(IUIRoot uiRoot, IUIFullFadePresenter fullFade)
	    {
		    this.uiRoot = uiRoot;
		    this.fullFade = fullFade;
	    }

	    public void Init(IUIService service)
	    {
		    uiService = service;
	    }

	    public IDisplayOption CreateShow(params object[] args)
        {
            return new ShowOptions(Get<WindowItem>(args), uiService, fullFade);
        }

        public IDisplayOption CreateHide(params object[] args)
        {
            return new HideOptions(Get<WindowItem>(args), uiService, uiRoot, fullFade);
        }

        public IDisplayOption CreateWrapper(IEnumerable<IDisplayOption> options)
        {
	        return new WrapperOptions(options);
        }
        private static T Get<T>(IEnumerable<object> args) => args.OfType<T>().FirstOrDefault() ?? default;
    }
}