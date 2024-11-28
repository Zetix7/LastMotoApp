using LastMotoApp.Data;

namespace LastMotoApp;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly MotoAppDbContext _motoAppDbContext;

    public App(IUserCommunication userCommunication, MotoAppDbContext motoAppDbContext)
    {
        _userCommunication = userCommunication;
        _motoAppDbContext = motoAppDbContext;
        _motoAppDbContext.Database.EnsureCreated();
    }

    public void Run()
    {
        _userCommunication.Run();
    }
}
