namespace Medium.ReplacingIfElse.Domain {
    public class Address {
        public Address(string streetName, string streetNumber) {
            StreetName = streetName;
            StreetNumber = streetNumber;
        }
        
        public string StreetName { get; private set; }
        public string StreetNumber { get; private set; } // string because they sometimes include letters
    }
}