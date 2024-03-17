package com.example.rest;

import java.io.Serializable;

public class Entity<ID> implements Serializable {
    private static final long serialVersionUID = 7331115341259248461L;
    private ID ID;

    public ID getId() {
        return ID;
    }

    public void setId(ID ID) {
        this.ID = ID;
    }
}
