
public class UIScene : UIBase
{
    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        //Managers_KSM.UIManager.SetCanvas(gameObject, false);
        return true;
    }
}