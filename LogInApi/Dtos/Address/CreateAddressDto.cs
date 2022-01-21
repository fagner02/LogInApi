namespace LogInApi.Dtos {
    public class CreateAddressDto {
        public string Street { get; set; }
        /// <summary>
        /// string : House Number
        /// </summary>
        /// <example>
        /// 1223
        /// </example>
        public string Number { get; set; }
        /// <summary>
        /// string : District Name
        /// </summary>
        /// <example>
        /// Centro
        /// </example>
        public string District { get; set; }
        /// <summary>
        /// string : City Name
        /// </summary>
        /// <example>
        /// Quixadá
        /// </example>
        public string City { get; set; }
        /// <summary>
        /// string : State Name
        /// </summary>
        /// <example>
        /// Ceará
        /// </example>
        public string State { get; set; }
    }
}