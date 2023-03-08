namespace multi_threadings.Routes;

public static class DummiesApi {
    public static RouteGroupBuilder MapDummiesApi(this RouteGroupBuilder group) {
        group.MapGet("/", () => "hello there");
        group.MapGet("/{id}", (string id) => $"hello there, {id}");
        return group;
    }



}
