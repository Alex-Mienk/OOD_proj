namespace Game.IItems;

public interface IItem
{
    char Symbol { get; }
    string Name { get; }
}

public interface IEquippable : IItem
{
    bool IsTwoHanded { get; }
}

public interface IWeapon : IEquippable
{
    int Damage { get; }
}

public interface ICurrency : IItem
{
    int Amount { get; }
}