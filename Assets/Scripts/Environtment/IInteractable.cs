namespace Project3D
{
    public interface IInteractable
    {
        void Interact(PlayerController player);
        bool CanInteract { get; set; }
        string Content { get; }
    }
}
