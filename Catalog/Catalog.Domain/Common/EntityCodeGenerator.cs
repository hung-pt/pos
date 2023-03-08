namespace Catalog.Domain.Common;

public static class EntityCodeGenerator {
    internal static int ProductInternalCode_01 = 1000; // todo should be on some db
    internal static int ProductInternalCode_02 = 1000;
    public static string GetNewProductCode() {
        // S72_3212
        return $"S{++ProductInternalCode_01}_{++ProductInternalCode_02}";
    }

    internal static int OrderNoSeq = 10425;
    public static int GetNewOrderNumber() {
        return ++OrderNoSeq;
    }

    internal static int CusNoSeq = 496;
    public static int GetNewCustomerNumber() {
        return ++CusNoSeq;
    }
}
