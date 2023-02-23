namespace Sam.Domain; 

public static class EntityCodeGenerator {
    private static int ProductInternalCode_01 = 1000; // should be on some db
    private static int ProductInternalCode_02 = 1000;
    public static string GetNewProductCode() {
        // S72_3212
        return $"S{ProductInternalCode_01++}_{ProductInternalCode_02++}";
    }
}
