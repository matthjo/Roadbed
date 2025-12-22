namespace Roadbed.Test.Unit.Crud.Mocks
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Roadbed.Crud.Repositories;

    /// <summary>
    /// CRUD repository for UnitCurrency entity.
    /// </summary>
    internal class UnitCurrencyRepository
        : IBaseRepositoryWithCrudl<UnitCurrencyRow, long>
    {
        #region Private Fields

        /// <summary>
        /// A lock object for thread safety when modifying the static list.
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Container for the in-memory list of currencies.
        /// </summary>
        private IList<UnitCurrencyRow> currencies = default!;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCurrencyRepository"/> class.
        /// </summary>
        public UnitCurrencyRepository()
        {
            // Initialize the in-memory list of countries.
            this.currencies = new List<UnitCurrencyRow>();

            // Add mock data
            this.currencies.Add(new UnitCurrencyRow(978, "Euro", "EUR"));
            this.currencies.Add(new UnitCurrencyRow(840, "United States Dollar", "USD"));
            this.currencies.Add(new UnitCurrencyRow(36, "Australian Dollar", "AUD"));
            this.currencies.Add(new UnitCurrencyRow(826, "United Kingdom Pound", "GBP"));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public Task<long> CreateAsync(UnitCurrencyRow dto, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                lock (LockObject)
                {
                    this.currencies.Add(new UnitCurrencyRow(dto.Id, dto.Name, dto.Code));
                }

                return dto.Id;
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                lock (LockObject)
                {
                    foreach (var currency in this.currencies)
                    {
                        if (currency.Id == id)
                        {
                            this.currencies.Remove(currency);
                            break;
                        }
                    }
                }
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task<IList<UnitCurrencyRow>> ListAsync(CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                return this.currencies;
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task<UnitCurrencyRow> ReadAsync(long id, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                UnitCurrencyRow result = default!;

                lock (LockObject)
                {
                    foreach (var currency in this.currencies)
                    {
                        if (currency.Id == id)
                        {
                            result = currency;
                            break;
                        }
                    }
                }

                return result;
            });

            return longTask;
        }

        /// <inheritdoc />
        public Task UpdateAsync(UnitCurrencyRow dto, CancellationToken cancellationToken)
        {
            var longTask = Task.Run(() =>
            {
                lock (LockObject)
                {
                    foreach (var currency in this.currencies)
                    {
                        if (currency.Id == dto.Id)
                        {
                            currency.Name = dto.Name;
                            currency.Code = dto.Code;
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