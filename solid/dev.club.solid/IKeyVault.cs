namespace dev.club.solid
{
    internal interface IKeyVault
    {
        Task<IKeyVaultClient> CreateClientAsync();
    }
}
