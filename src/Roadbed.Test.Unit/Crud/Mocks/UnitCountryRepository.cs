namespace Roadbed.Test.Unit.Crud.Mocks
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Roadbed.Crud.Repositories;

    /// <summary>
    /// CRUD repository for UnitCountry entity.
    /// </summary>
    internal class UnitCountryRepository
        : IBaseRepositoryWithCrud<UnitCountryRow, int>
    {
        #region Private Fields

        /// <summary>
        /// A lock object for thread safety when modifying the static list.
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Container for the in-memory list of countries.
        /// </summary>
        private IList<UnitCountryRow> countries = new List<UnitCountryRow>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCountryRepository"/> class.
        /// </summary>
        public UnitCountryRepository()
        {
            // Initialize the in-memory list of countries.
            this.countries = new List<UnitCountryRow>();

            // Add mock data
            this.countries.Add(new UnitCountryRow(724, "Spain", "ES"));
            this.countries.Add(new UnitCountryRow(752, "Sweden", "SE"));
            this.countries.Add(new UnitCountryRow(756, "Switzerland", "CH"));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public Task<int> CreateAsync(UnitCountryRow dto, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                lock (LockObject)
                {
                    this.countries.Add(new UnitCountryRow(dto.Id, dto.Name, dto.Code));
                }

                return dto.Id;
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                lock (LockObject)
                {
                    foreach (var country in this.countries)
                    {
                        if (country.Id == id)
                        {
                            this.countries.Remove(country);
                            break;
                        }
                    }
                }
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task<UnitCountryRow> ReadAsync(int id, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                UnitCountryRow result = default!;

                lock (LockObject)
                {
                    foreach (var country in this.countries)
                    {
                        if (country.Id == id)
                        {
                            result = country;
                            break;
                        }
                    }
                }

                return result;
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task UpdateAsync(UnitCountryRow dto, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                lock (LockObject)
                {
                    foreach (var country in this.countries)
                    {
                        if (country.Id == dto.Id)
                        {
                            country.Name = dto.Name;
                            country.Code = dto.Code;
                            break;
                        }
                    }
                }
            });

            return longTask;
        }

        #endregion Public Methods
    }
}