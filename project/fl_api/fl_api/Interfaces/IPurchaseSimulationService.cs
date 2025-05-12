using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IPurchaseSimulationService
    {
        /// <summary>
        /// Genera una simulación de órdenes de compra
        /// basadas en la demanda entre `from` y `to`.
        /// </summary>
        Task<PurchaseSimulationDto> SimulateAsync(DateTime from, DateTime to);
    }
}
