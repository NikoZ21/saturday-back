namespace Saturday_Back.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        public int BenefitTypeId { get; set; }
        public BenefitType? BenefitType { get; set; }

        public int BaseCostId { get; set; }
        public BaseCost? BaseCost { get; set; }
    }
}