namespace Roadbed.Test.Unit.Crud.Mocks
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Roadbed.Crud.Repositories;

    /// <summary>
    /// CRUD repository for UnitLanguage entity.
    /// </summary>
    internal class UnitLanguageRepository
        : IBaseRepositoryWithListOnly<UnitLanguageRow, string>
    {
        #region Private Fields

        /// <summary>
        /// A lock object for thread safety when modifying the static list.
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Container for the in-memory list of langauges.
        /// </summary>
        private IList<UnitLanguageRow> languages = default!;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitLanguageRepository"/> class.
        /// </summary>
        public UnitLanguageRepository()
        {
            // Initialize the in-memory list of langauges.
            this.languages = new List<UnitLanguageRow>();

            // Add mock data
            this.languages.Add(new UnitLanguageRow("cy", "Welsh"));
            this.languages.Add(new UnitLanguageRow("cs", "Czech"));
            this.languages.Add(new UnitLanguageRow("da", "Danish"));
            this.languages.Add(new UnitLanguageRow("de", "German"));
            this.languages.Add(new UnitLanguageRow("fr", "French"));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public Task<IList<UnitLanguageRow>> ListAsync(CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                return this.languages;
            });

            return longTask;
        }

        #endregion Public Methods
    }
}