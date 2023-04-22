export const baseUrl = "";

export const loginUrl = baseUrl + "/auth/login";
export const refreshTokenUrl = baseUrl + "/auth/refresh-token";

export const eventUrl = baseUrl + "/event";
export const eventByIdUrl = (id) => `${eventUrl}/${id}`;

export const eventRegisterUrl = eventUrl + "/register";
export const eventRegisteredUsersUrl = (id) => eventByIdUrl(id) + "/registered-users";
export const eventParticipantsUrl = (id) => eventByIdUrl(id) + "/participant";

export const eventTypeUrl = baseUrl + "/event-type";
export const eventAllTypesUrl = eventTypeUrl + "/all";

export const userUrl = baseUrl + "/user";
export const userByIdUrl = (id) => `${userUrl}/${id}`;