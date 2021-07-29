using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Data.Services
{
    public class ReservationJsonService : IReservationService
    {
        private List<Reservation> Reservations { get; set; }
        private string _jsonFileName;

        public ReservationJsonService()
        {
            Reservations = new List<Reservation>();
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Data/reservations.json";
            GetReservationsFromFile();
        }

        private void GetReservationsFromFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                Reservations = JsonSerializer.Deserialize<List<Reservation>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public void AddReservation(Reservation reservation)
        {
            Reservations.Add(reservation);
            SaveReservationsToFile();
        }

        private void SaveReservationsToFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<Reservation>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , Reservations);
            }
        }
        public List<Reservation> GetReservations()
        {
            return Reservations;
        }
    }
}
