package domain;

import java.util.Objects;

public class Participant extends Entity<Long>{
    private String nume;
    private String prenume;

    public Participant(String nume, String prenume) {
        this.nume = nume;
        this.prenume = prenume;
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

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Participant that)) return false;
        return getNume().equals(that.getNume()) && getPrenume().equals(that.getPrenume());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getNume(), getPrenume());
    }

    @Override
    public String toString() {
        return "Participant{" +
                "nume='" + nume + '\'' +
                ", prenume='" + prenume + '\'' +
                '}';
    }
}
