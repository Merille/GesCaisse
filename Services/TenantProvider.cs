namespace EasytransitCaisse.Services
{
    public class TenantProvider : ITenantProvider
    {
        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            var claim = user?.FindFirst(AppClaimTypes.TenantId)?.Value;

            TenantId = int.TryParse(claim, out var id) ? id : 0;
        }

        public int TenantId { get; }

        public bool IsSuperAdmin => TenantId == 0;
    }
}
