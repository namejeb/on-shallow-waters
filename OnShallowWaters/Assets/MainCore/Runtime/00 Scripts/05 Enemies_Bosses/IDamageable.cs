namespace _04_Scripts._05_Enemies_Bosses {
    public interface IDamageable{
        void Damage(int damageAmount);

        float LostHP();
    }
}