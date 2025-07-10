using Microsoft.AspNetCore.Mvc;

//Abstracted routing class. Helps neatly wrap API endpoint routing into each business logic class that concerns them. This is done by implementing the IRouter interface where endpoints are being defined and running the Map methods in the MapRoutes method
//in each file for the endpoints being defined in them.
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

    //This fella gets called in the program file. It grabs all of the defined router services (defined and dependancy injected in program.cs) and calls their MapRoutes method. Each of these MapRoute methods should contain the routes for the implemented methods
    //being used. This lets us seperate the concern of routing to its respective implementing file and makes things nice and modular.
    public void RouteEndpoints(WebApplication app)
    {
        foreach (var router in _routers)
        {
            router.MapRoutes(app);
        }
    }
}
