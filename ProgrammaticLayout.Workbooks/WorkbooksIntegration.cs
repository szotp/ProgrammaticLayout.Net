using Xamarin.Interactive;

[assembly: AgentIntegration(typeof(ProgrammaticLayout.WorkbooksIntegration))]
namespace ProgrammaticLayout
{

    public class WorkbooksIntegration : IAgentIntegration
    {
        public void IntegrateWith(IAgent agent)
        {
            agent.RepresentationManager.AddProvider(new ProgrammaticLayoutRepresentationProvider());
        }
    }
}
