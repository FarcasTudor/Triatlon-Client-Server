package domain;

import java.util.Objects;

public class Proba extends Entity<Long>{
    private String numeProba;
    private Long idArbitru;


    public Proba(String numeProba, Long idArbitru) {
        this.numeProba = numeProba;
        this.idArbitru = idArbitru;
    }

    public String getNumeProba() {
        return numeProba;
    }

    public void setNumeProba(String numeProba) {
        this.numeProba = numeProba;
    }

    public Long getIdArbitru() {
        return idArbitru;
    }

    public void setIdArbitru(Long idArbitru) {
        this.idArbitru = idArbitru;
    }


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Proba proba)) return false;
        return getNumeProba().equals(proba.getNumeProba()) && getIdArbitru().equals(proba.getIdArbitru());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getNumeProba(), getIdArbitru());
    }

    @Override
    public String toString() {
        return "Proba{" +
                "numeProba='" + numeProba + '\'' +
                ", idArbitru=" + idArbitru +
                '}';
    }
}
