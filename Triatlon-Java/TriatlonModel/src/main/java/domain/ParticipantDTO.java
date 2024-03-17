package domain;

import java.util.Objects;

public class ParticipantDTO extends Entity<Long>{

    private String nume;
    private String prenume;
    private Long punctajTotal;

    public ParticipantDTO(Long id, String nume, String prenume, Long punctajTotal) {
        super(id);
        this.nume = nume;
        this.prenume = prenume;
        this.punctajTotal = punctajTotal;
    }

    public String getNume() {
        return nume;
    }

    public void setNume(String nume) {
        this.nume = nume;
    }

    public String getPrenume() {
        return prenume;
    }

    public void setPrenume(String prenume) {
        this.prenume = prenume;
    }

    public Long getPunctajTotal() {
        return punctajTotal;
    }

    public void setPunctajTotal(Long punctajTotal) {
        this.punctajTotal = punctajTotal;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof ParticipantDTO that)) return false;
        return Objects.equals(getNume(), that.getNume()) && Objects.equals(getPrenume(), that.getPrenume()) && Objects.equals(getPunctajTotal(), that.getPunctajTotal());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getNume(), getPrenume(), getPunctajTotal());
    }

    @Override
    public String toString() {
        return "ParticipantDTO{" +
                "nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                ", punctajTotal=" + punctajTotal +
                '}';
    }
}
