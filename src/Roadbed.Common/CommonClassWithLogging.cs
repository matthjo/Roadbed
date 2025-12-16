/*
 * The namespace Roadbed.Common was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */
namespace Roadbed
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// Base class with logging implemented.
    /// </summary>
    /// <typeparam name="TCategoryName">Type inheriting from the Base.</typeparam>
    [Serializable]
    public abstract class CommonClassWithLogging<TCategoryName>
    {
        #region Private Fields

        /// <summary>
        /// Container for the public property Logger.
        /// </summary>
        [NonSerialized]
        private ILogger<TCategoryName> logger;

        /// <summary>
        /// Container for the public property LoggerFactory.
        /// </summary>
        [NonSerialized]
        private ILoggerFactory loggerFactory;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonClassWithLogging{TCategoryName}"/> class.
        /// </summary>
        protected CommonClassWithLogging()
        {
            this.loggerFactory = NullLoggerFactory.Instance;
            this.logger = NullLogger<TCategoryName>.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonClassWithLogging{TTCategoryName}"/> class.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        protected CommonClassWithLogging(ILogger logger)
        {
            this.loggerFactory = NullLoggerFactory.Instance;
            this.logger = logger as ILogger<TCategoryName> ?? NullLogger<TCategoryName>.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonClassWithLogging{TCategoryName}"/> class.
        /// </summary>
        /// <param name="loggerFactory">Represents a type used to configure the logging system and create instances of ILogger from the registered ILoggerProviders.</param>
        protected CommonClassWithLogging(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
            this.logger = this.loggerFactory.CreateLogger<TCategoryName>();
        }

        #endregion Public Constructors

        #region Protected Properties

        /// <summary>
        /// Gets or sets the type used to perform logging.
        /// </summary>
        public ILogger<TCategoryName> Logger
        {
            get
            {
                return this.logger;
            }

            set
            {
                this.logger = value;
            }
        }

        /// <summary>
        /// Gets or sets the type used to configure the logging system and create instances of ILogger from the registered ILoggerProviders.
        /// </summary>
        public ILoggerFactory LoggerFactory
        {
            get
            {
                return this.loggerFactory;
            }

            set
            {
                this.loggerFactory = value;
            }
        }

        #endregion Protected Properties

    }
}