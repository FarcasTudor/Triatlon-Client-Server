package domain;

import java.util.Objects;

public class Inscriere extends Entity<Long>{
    private Long idParticipant;
    private Long idProba;

    private Long punctaj;

    public Inscriere(Long idParticipant, Long idProba, Long punctaj) {
        this.idParticipant = idParticipant;
        this.idProba = idProba;
        this.punctaj = punctaj;
    }


    public Long getIdParticipant() {
        return idParticipant;
    }

    public void setIdParticipant(Long idParticipant) {
        this.idParticipant = idParticipant;
    }

    public Long getIdProba() {
        return idProba;
    }

    public void setIdProba(Long idProba) {
        this.idProba = idProba;
    }

    public Long getPunctaj() {
        return punctaj;
    }

    public void setPunctaj(Long punctaj) {
        this.punctaj = punctaj;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Inscriere inscriere)) return false;
        return getPunctaj() == inscriere.getPunctaj() && Objects.equals(getIdParticipant(), inscriere.getIdParticipant()) && Objects.equals(getIdProba(), inscriere.getIdProba());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getIdParticipant(), getIdProba(), getPunctaj());
    }

    @Override
    public String toString() {
        return "Inscriere{" +
                "idParticipant=" + idParticipant +
                ", idProba=" + idProba +
                ", punctaj=" + punctaj +
                '}';
    }
}
