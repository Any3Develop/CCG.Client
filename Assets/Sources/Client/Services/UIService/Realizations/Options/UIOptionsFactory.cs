using Client.Services.UIService.FullFade;

namespace Client.Services.UIService.Options
{
    public class UIOptionsFactory : IUIOptionFactory
    {
	    private readonly IUIRoot uiRoot;
	    private readonly IUIFullFadePresenter fullFade;
	    private readonly IUIServiceRepository serviceRepository;
	    private IUIService uiService;

	    public UIOptionsFactory(
		    IUIRoot uiRoot, 
		    IUIFullFadePresenter fullFade, 
		    IUIServiceRepository serviceRepository)
	    {
		    this.uiRoot = uiRoot;
		    this.fullFade = fullFade;
		    this.serviceRepository = serviceRepository;
	    }

	    public void Init(IUIService service)
	    {
		    uiService = service;
	    }

	    public IUIOptions<T> Create<T>() where T : IUIWindow
	    {
		    return new DefaultUIOptions<T>(uiRoot, uiService, fullFade, serviceRepository);
	    }
    }
}