export class BrigadeMemberModel {
    constructor({
        brigadeMemberId = 0,
        personId = 0,
        brigadeId = 0,
        roleId = 0,
        memberSpecializationTypeId = 0,
        personFullName = "",
        brigadeName = "",
        roleName = "",
        specializationTypeName = ""
    } = {}) {
        this.brigadeMemberId = brigadeMemberId;
        this.personId = personId;
        this.brigadeId = brigadeId;
        this.roleId = roleId;
        this.memberSpecializationTypeId = memberSpecializationTypeId;
        this.personFullName = personFullName;
        this.brigadeName = brigadeName;
        this.roleName = roleName;
        this.specializationTypeName = specializationTypeName;
    }
}