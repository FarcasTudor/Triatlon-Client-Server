using System;
using System.Collections.Generic;
using TriatlonModel;
using proto = Triatlon.Protocol;

namespace Triatlon.Protocol
{
    static class ProtoUtils
    {
        public static TriatlonResponse createOkResponse()
        {
            TriatlonResponse response = new TriatlonResponse { Type = TriatlonResponse.Types.Type.Ok };
            return response;
        }

        public static TriatlonResponse createErrorResponse(String text)
        {
            TriatlonResponse response = new TriatlonResponse{
                Type=TriatlonResponse.Types.Type.Error, Error=text};
            return response;
        }
        

        public static TriatlonModel.ArbitruDTO getArbitruDTO(TriatlonRequest request)
        {
            TriatlonModel.ArbitruDTO arbDTO = new TriatlonModel.ArbitruDTO(request.ArbitruDTO.Username, request.ArbitruDTO.Parola);
            return arbDTO;
        }

        public static TriatlonResponse createLogInResponse(TriatlonModel.Arbitru arbi)
        {
            proto.Arbitru arbitru = new proto.Arbitru
            {
                Id = arbi.getId(),
                Nume = arbi.Nume,
                Prenume = arbi.Prenume,
                Username = arbi.Username,
                Parola = arbi.Parola
            };
            TriatlonResponse response = new TriatlonResponse { Type = TriatlonResponse.Types.Type.Ok, Arbitru = arbitru };
            return response;
        }

        public static TriatlonModel.Arbitru getArbitru(TriatlonRequest request)
        {
            TriatlonModel.Arbitru arb = new TriatlonModel.Arbitru(request.Arbitru.Nume, request.Arbitru.Prenume, request.Arbitru.Username, request.Arbitru.Parola);
            arb.setId(request.Arbitru.Id);
            return arb;
        }

        public static TriatlonResponse createGetParticipantiCuPunctajResponse(List<TriatlonModel.ParticipantDTO> participanti)
        {
            TriatlonResponse response = new TriatlonResponse { Type = TriatlonResponse.Types.Type.TakeParticipantiCuPunctaj };
            foreach(var part in participanti)
            {
                proto.ParticipantDTO partDTO = new proto.ParticipantDTO
                {
                    Id = part.getId(), Nume = part.Nume, Prenume = part.Prenume, Punctaj = part.PunctajTotal
                };
                response.ParticipantiDTO.Add(partDTO);
            }

            return response;
        }

        public static TriatlonResponse createGetProbeResponse(IEnumerable<TriatlonModel.Proba> probe)
        {
            TriatlonResponse response = new TriatlonResponse { Type = TriatlonResponse.Types.Type.TakeProbe };
            foreach(TriatlonModel.Proba prob in probe)
            {
                proto.Proba proba = new proto.Proba
                {
                    Id = prob.getId(), NumeProba = prob.NumeProba, IdArbitru = prob.IdArbitru
                };
                response.Probe.Add(proba);
            }
            
            return response;
        }

        public static TriatlonModel.Proba getProba(TriatlonRequest request)
        {
            TriatlonModel.Proba proba = new TriatlonModel.Proba(request.Proba.NumeProba, request.Proba.IdArbitru);
            proba.setId(request.Proba.Id);
            return proba;
        }

        public static TriatlonResponse createGetParticipantiDeLaProbaResponse(List<TriatlonModel.ParticipantDTO> participanti)
        {
            TriatlonResponse response = new TriatlonResponse
                { Type = TriatlonResponse.Types.Type.TakeParticipantiDeLaProba };
            foreach (var part in participanti)
            {
                proto.ParticipantDTO partDTO = new proto.ParticipantDTO
                {
                    Id = part.getId(), Nume = part.Nume, Prenume = part.Prenume, Punctaj = part.PunctajTotal
                };
                response.ParticipantiDTO.Add(partDTO);
            }   
            return response;
        }

        public static TriatlonModel.Inscriere getInscriere(TriatlonRequest request)
        {
            TriatlonModel.Inscriere inscriere = new TriatlonModel.Inscriere(request.Inscriere.IdParticipant, request.Inscriere.IdProba, request.Inscriere.Punctaj);
            return inscriere;
        }

        public static TriatlonResponse createUpdatePunctajResponse()
        {
            TriatlonResponse response = new TriatlonResponse { Type = TriatlonResponse.Types.Type.UpdatePunctaj };
            Console.WriteLine("Update punctaj response from proto utils");
            return response;
        }
    }
}