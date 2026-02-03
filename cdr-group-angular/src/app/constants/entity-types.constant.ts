export const EntityTypes = {
  EMPLOYEE: 'Employee'
} as const;

export type EntityType = typeof EntityTypes[keyof typeof EntityTypes];
