public class Barracks : House
{
    public override void Setup()
    {
        base.Setup();
        _character.Vilager.SetSpecialization(Specialization.Soldier);
    }
}
