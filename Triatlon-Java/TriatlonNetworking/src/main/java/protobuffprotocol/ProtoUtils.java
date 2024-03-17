package protobuffprotocol;
import domain.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ProtoUtils {

    public static TriatlonProtobufs.TriatlonRequest createLoginRequest(ArbitruDTO arbitru) {
        TriatlonProtobufs.ArbitruDTO arb=TriatlonProtobufs.ArbitruDTO.newBuilder().setUsername(arbitru.getUsername()).setParola(arbitru.getParola()).build();
        TriatlonProtobufs.TriatlonRequest request= TriatlonProtobufs.TriatlonRequest.newBuilder().setType(TriatlonProtobufs.TriatlonRequest.Type.LOGIN)
                .setArbitruDTO(arb).build();
        return request;
    }

    public static TriatlonProtobufs.TriatlonRequest createLogoutRequest(Arbitru arbitru){
        TriatlonProtobufs.Arbitru arb=TriatlonProtobufs.Arbitru.newBuilder().setId(arbitru.getId()).setNume(arbitru.getNume()).setPrenume(arbitru.getPrenume()).setUsername(arbitru.getUsername()).setParola(arbitru.getParola()).build();
        TriatlonProtobufs.TriatlonRequest request= TriatlonProtobufs.TriatlonRequest.newBuilder().setType(TriatlonProtobufs.TriatlonRequest.Type.LOGOUT)
                .setArbitru(arb).build();
        return request;
    }

    public static String getError(TriatlonProtobufs.TriatlonResponse response) {
        String errorMessage = response.getError();
        return errorMessage;
    }
    public static Inscriere getInscriere(TriatlonProtobufs.TriatlonResponse updateResponse) {
        return null;

    }

    public static Arbitru getArbitru(TriatlonProtobufs.TriatlonResponse response) {
        Arbitru arbitru = new Arbitru(response.getArbitru().getNume(),response.getArbitru().getPrenume(),response.getArbitru().getUsername(),response.getArbitru().getParola());
        arbitru.setId(response.getArbitru().getId());
        return arbitru;
    }


/*
    public static List<Proba> getProbe(TriatlonProtobufs.TriatlonResponse response) {
        List<Proba> probe = new ArrayList<>();
        for (int i = 0; i < response.getProbeCount(); i++) {
            TriatlonProtobufs.Proba probaP = response.getProbe(i);

            Long idArbitru = probaP.getIdArbitru();
            String numeProba = probaP.getNumeProba();

            Proba proba = new Proba(numeProba, idArbitru);
            proba.setId(probaP.getId());
            probe.add(proba);
        }
        return probe;
    }*/

    public static TriatlonProtobufs.TriatlonRequest createGetParticipantiCuPunctajRequest() {
        TriatlonProtobufs.TriatlonRequest request = TriatlonProtobufs.TriatlonRequest.newBuilder().setType(TriatlonProtobufs.TriatlonRequest.Type.GET_PARTICIPANTI_CU_PUNCTAJ).build();
        return request;
    }

    public static List<ParticipantDTO> createGetParticipantiCuPunctaj(TriatlonProtobufs.TriatlonResponse response) {
        List<ParticipantDTO> participanti = new ArrayList<>();
        for (int i = 0; i < response.getParticipantiDTOCount(); i++) {
            TriatlonProtobufs.ParticipantDTO partDTO = response.getParticipantiDTO(i);
            ParticipantDTO participant = new ParticipantDTO(partDTO.getId(), partDTO.getNume(),partDTO.getPrenume(), partDTO.getPunctaj());
            participanti.add(participant);
        }
        return participanti;
    }

    public static TriatlonProtobufs.TriatlonRequest createGetParticipantiDeLaProbaRequest(Proba proba) {
        TriatlonProtobufs.Proba probaP = TriatlonProtobufs.Proba.newBuilder().setId(proba.getId()).setNumeProba(proba.getNumeProba()).setIdArbitru(proba.getIdArbitru()).build();
        TriatlonProtobufs.TriatlonRequest request = TriatlonProtobufs.TriatlonRequest.newBuilder().setType(TriatlonProtobufs.TriatlonRequest.Type.GET_PARTICIPANTI_DE_LA_PROBA).setProba(probaP).build();
        return request;
    }

    public static List<ParticipantDTO> createGetParticipantiDeLaProba(TriatlonProtobufs.TriatlonResponse response) {
        List<ParticipantDTO> participanti = new ArrayList<>();
        for(int i = 0; i < response.getParticipantiDTOCount(); i++) {
            TriatlonProtobufs.ParticipantDTO partDTO = response.getParticipantiDTO(i);
            ParticipantDTO part = new ParticipantDTO(partDTO.getId(), partDTO.getNume(), partDTO.getPrenume(), partDTO.getPunctaj());
            participanti.add(part);
        }
        return participanti;
    }

    public static TriatlonProtobufs.TriatlonRequest createGetProbeRequest() {
        TriatlonProtobufs.TriatlonRequest request = TriatlonProtobufs.TriatlonRequest.newBuilder().setType(TriatlonProtobufs.TriatlonRequest.Type.GET_PROBE).build();
        return request;
    }

    public static List<Proba> createGetProbe(TriatlonProtobufs.TriatlonResponse response) {
        List<Proba> probe = new ArrayList<>();
        for(int i = 0; i < response.getProbeCount(); i++) {
            TriatlonProtobufs.Proba probaP = response.getProbe(i);
            Proba proba = new Proba(probaP.getNumeProba(), probaP.getIdArbitru());
            proba.setId(probaP.getId());
            probe.add(proba);
        }
        return probe;

    }

    public static TriatlonProtobufs.TriatlonRequest createAddInscriereRequest(Inscriere inscriere) {
        TriatlonProtobufs.Inscriere inscriereP = TriatlonProtobufs.Inscriere.newBuilder().setIdParticipant(inscriere.getIdParticipant()).setIdProba(inscriere.getIdProba()).setPunctaj(inscriere.getPunctaj()).build();
        TriatlonProtobufs.TriatlonRequest request = TriatlonProtobufs.TriatlonRequest.newBuilder().setType(TriatlonProtobufs.TriatlonRequest.Type.ADD_INSCRIERE).setInscriere(inscriereP).build();
        return request;
    }
}
