









//public interface ICova<out T> { }
//public interface IContra<in T> { }

//public class Cova<T> : ICova<T> { }
//public class Contra<T> : IContra<T> { }

//public class Fruit { }
//public class Apple : Fruit { }

//public class TheInsAndOuts {
//    public void Covariance() {
//        ICova<Fruit> fruit = new Cova<Fruit>();
//        ICova<Apple> apple = new Cova<Apple>();

//        DoCova(fruit);
//        DoCova(apple); //apple is being upcasted to fruit, without the out keyword this will not compile
//    }

//    public void Contravariance() {
//        IContra<Fruit> fruit = new Contra<Fruit>();
//        IContra<Apple> apple = new Contra<Apple>();

//        DoContra(fruit); //fruit is being downcasted to apple, without the in keyword this will not compile
//        DoContra(apple);
//    }

//    public void DoCova(ICova<Fruit> fruit) { } // this type or derived
//    public void DoContra(IContra<Apple> apple) { } // this type or its base
//}
