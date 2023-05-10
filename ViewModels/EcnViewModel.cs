using ECN.Contracts.Services;
using ECN.Contracts.Views;
using ECN.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class EcnViewModel : ViewModelBase
    {

        private readonly IEcnDataService _ecnDataService;
        private readonly IWindowManagerService _windowManagerService;

        private Cita _Cita;
        public Cita Cita
        {
            get => _Cita;
            set
            {
                if (_Cita != value)
                {
                    _Cita = value;
                    RaisePropertyChanged("Cita");
                }
            }
        }


        public RelayCommand SaveCitaCommand { get; set; }

  

        public EcnViewModel(IEcnDataService ecnDataService, IWindowManagerService windowManagerService)
        {
            _ecnDataService = ecnDataService;
            _windowManagerService = windowManagerService;


            Cita = new Cita
            {
                IdUsuario = UserRecord.User_ID,
                FechaHora = DateTime.Today,
                IdEstado = 2,
            };



            GetPacientes();
            GetMedicos();

            SaveCitaCommand = new RelayCommand(SaveCita);


        }

        private void SaveCita()
        {
            Cita.IdMedicoNavigation = MedicoSeleccionado;
            Cita.IdPacienteNavigation = PacienteSeleccionado;

            try
            {
                if (_ecnDataService.SaveCita(Cita))
                {

                    _ = _windowManagerService.OpenInDialog(typeof(EcnRegistrationViewModel).FullName, Cita.IdCita);
                    ResetData();
                }

            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + ex.ToString());
            }
        }


        private void ResetData()
        {
            Cita = new Cita
            {
                IdUsuario = UserRecord.User_ID,
                FechaHora = DateTime.Today,
                IdEstado = 2
            };

            MedicoSeleccionado = null;
            PacienteSeleccionado = null;

        }



        private ObservableCollection<Paciente> _Pacientes;
        public ObservableCollection<Paciente> Pacientes
        {
            get => _Pacientes;
            set
            {
                if (_Pacientes != value)
                {
                    _Pacientes = value;
                    RaisePropertyChanged("Pacientes");
                }
            }
        }

        private ObservableCollection<Usuario> _Medicos;
        public ObservableCollection<Usuario> Medicos
        {
            get => _Medicos;
            set
            {
                if (_Medicos != value)
                {
                    _Medicos = value;
                    RaisePropertyChanged("Medicos");
                }
            }
        }

        
        private Usuario _MedicoSeleccionado;
        public Usuario MedicoSeleccionado
        {
            get => _MedicoSeleccionado;
            set
            {
                if (_MedicoSeleccionado != value)
                {
                    _MedicoSeleccionado = value;
                    RaisePropertyChanged("MedicoSeleccionado");

                }
            }
        }

        private Paciente _PacienteSeleccionado;
        public Paciente PacienteSeleccionado
        {
            get => _PacienteSeleccionado;
            set
            {
                if (_PacienteSeleccionado != value)
                {
                    _PacienteSeleccionado = value;
                    RaisePropertyChanged("PacienteSeleccionado");

                }
            }
        }


        public async void GetPacientes()
        {
            Pacientes = new ObservableCollection<Paciente>();

            var data = await _ecnDataService.GetPacientesAsync();
            foreach (var item in data)
            {
                Pacientes.Add(item);
            }
        }

        public async void GetMedicos()
        {
            Medicos = new ObservableCollection<Usuario>();

            var data = await _ecnDataService.GetMedicosAsync();
            foreach (var item in data)
            {
                Medicos.Add(item);
            }
        }



    }
}
