using Microsoft.AspNetCore.Mvc;

public class PointRouter
{
    private readonly IEnumerable<IRouter> _routers;

    public interface IRouter
    {
        void MapRoutes(WebApplication app);
    }

    public PointRouter(IEnumerable<IRouter> routers)
    {
        _routers = routers;
    }

    public void RouteEndpoints(WebApplication app)
    {
        foreach (var router in _routers)
        {
            router.MapRoutes(app);
        }
    }
}
