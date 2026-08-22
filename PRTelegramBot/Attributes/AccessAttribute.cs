namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Attribute that checks access rights for running methods.
    /// </summary>
    public class AccessAttribute : Attribute
    {
        #region Fields and properties

        /// <summary>
        /// Access mask.
        /// </summary>
        public int Mask { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mask">Access mask.</param>
        public AccessAttribute(int mask)
        {
            Mask = mask;
        }

        #endregion
    }
}
