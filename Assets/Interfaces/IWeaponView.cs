namespace AsteroidsECS
{
    public interface IWeaponView
    {
        void InitializeView();
        void Vizualize();
        void UpdateAmmoInfo(float value);
        void DisplayMaxAmmoValue(float value);
        void DisableVizualization();
    }
}
